import React, {useEffect} from 'react';
import {useLazyQuery} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {Col, Row, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link, useSearchParams} from 'react-router-dom';
import Title from 'antd/es/typography/Title';
import {GET_HOMEWORKS_QUERY, GetHomeworksData, GetHomeworksVars} from '../../../../../../gql/modules/homeworks/homeworks.queries';
import {Homework, HomeworkStatus} from '../../../../../../gql/modules/homeworks/homework.types';
import {stringToUkraineDatetime} from '../../../../../../convertors/stringToDatetimeConvertors';
import {Order} from '../../../../../../gql/enums/order';
import {homeworkStatusToTag} from '../../../../../../convertors/enumToTagConvertor';
import {homeworkStatusWithTranslateToString} from '../../../../../../convertors/enumWithTranslateToStringConvertor';

export const HomeworksIndex = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [getHomeworks, getHomeworksOptions] = useLazyQuery<GetHomeworksData, GetHomeworksVars>(GET_HOMEWORKS_QUERY,
        {fetchPolicy: 'network-only'},
    );
    // const [removeSubjectMutation, removeSubjectMutationOptions] = useMutation<RemoveSubjectData, RemoveSubjectVars>(REMOVE_SUBJECT_MUTATION);

    useEffect(() => {
        const page = parseInt(searchParams.get('page') || '') || 1;
        const statuses = searchParams.get('statuses')?.split('|').filter(s => Object.values(HomeworkStatus).includes(s as HomeworkStatus)) as HomeworkStatus[];
        const orderString = searchParams.get('order') || '';
        const order = Object.values(Order).includes(orderString?.toUpperCase() as Order)
            ? Order[orderString.charAt(0).toUpperCase() + orderString.toLowerCase().slice(1) as keyof typeof Order || Order.Descend]
            : Order.Descend;
        getHomeworks({
            variables: {
                page,
                statuses: statuses,
                order: order
            },
        });
    }, [searchParams]);

    const onRemove = (subjectId: string) => {
        // removeSubjectMutation({variables: {id: subjectId}})
        //     .then(async (response) => {
        //         const page = parseInt(searchParams.get('page') || '') || 1;
        //         const like = searchParams.get('like') || '';
        //         await getSubjects({variables: {page, like}});
        //     })
        //     .catch(error => {
        //         message.error(error.message);
        //     });
    };

    const columns: ColumnsType<Homework> = [
        {
            title: 'Пост',
            dataIndex: 'subjectPost',
            key: 'subjectPost',
            render: (text, homework) => <>{homework?.subjectPost?.title}</>,
        },
        {
            title: 'Виконав',
            dataIndex: 'student',
            key: 'student',
            render: (text, homework) => <>{homework?.student?.lastName} {homework?.student?.firstName}</>,
        },
        {
            title: 'Оцінка',
            dataIndex: 'mark',
            key: 'mark',
        },
        {
            title: 'Статус',
            dataIndex: 'status',
            key: 'status',
            render: (text, homework) => homeworkStatusToTag(text),
            filters: (Object.values(HomeworkStatus) as Array<HomeworkStatus>).map((value) => ({
                value: value,
                text: homeworkStatusWithTranslateToString(value),
            })),
            defaultFilteredValue: searchParams.get('statuses')?.split('|'),
        },
        {
            title: 'Надіслано',
            dataIndex: 'createdAt',
            key: 'createdAt',
            render: (text, homework) => stringToUkraineDatetime(text),
            sorter: true,
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            width: '130px',
            render: (text, homework) => (
                // (currentUser?.id === subject.teacherId || currentUser?.role === Role.Administrator)
                //     ? <ButtonsVUR viewUrl={`${subject?.id}`} updateUrl={`update/${subject?.id}`}
                //                   onRemove={() => onRemove(subject?.id)}/>
                //     : <ButtonsVUR viewUrl={`${subject?.id}`}/>
                <ButtonsVUR viewUrl={`${homework?.id}`} updateUrl={`update/${homework?.id}`}
                            onRemove={() => onRemove(homework?.id)}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Домашні роботи</Title>
            <Row justify="space-between">
                <Col>
                    <Link to={'create'}>
                        <ButtonCreate/>
                    </Link>
                </Col>
                <Col>
                </Col>
            </Row>
            <Table
                rowKey={'id'}
                loading={getHomeworksOptions.loading /*|| removeSubjectMutationOptions.loading*/}
                dataSource={getHomeworksOptions.data?.getHomeworks.entities}
                columns={columns}
                pagination={{
                    current: parseInt(searchParams.get('page') || '') || 1,
                    defaultPageSize: getHomeworksOptions.data?.getHomeworks.pageSize,
                    total: getHomeworksOptions.data?.getHomeworks.total,
                    onChange: page => setSearchParams({page: page.toString()}),
                }}
                onChange={(pagination, filters, sorter: any) => {
                    console.log(pagination, filters, sorter);
                    setSearchParams({statuses: filters.status?.join('|') || '', order: sorter.order});
                }}
            />
        </Space>
    );
};
