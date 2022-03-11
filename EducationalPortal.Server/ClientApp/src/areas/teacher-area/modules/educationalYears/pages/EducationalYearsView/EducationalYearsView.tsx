import React, {useState} from 'react';
import {useQuery} from '@apollo/client';
import {
    GET_EDUCATIONAL_YEAR_WITH_SUBJECTS_QUERY,
    GetEducationalYearWithSubjectsData,
    GetEducationalYearWithSubjectsVars,
} from '../../educationalYears.queries';
import {Link, Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {Space, Table, Tag} from 'antd';
import Title from 'antd/es/typography/Title';
import '../../../../../../styles/table.css';
import {stringToUkraineDate} from '../../../../../../convertors/stringToDatetimeConvertors';
import {ColumnsType} from 'antd/es/table';
import {Subject} from '../../../subjects/subjects.types';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {useAppSelector} from '../../../../../../store/store';

export const EducationalYearsView = () => {
    const currentUser = useAppSelector(s => s.auth.me?.user);
    const params = useParams();
    const id = params.id as string;
    const [subjectsPage, setSubjectsPage] = useState(1);
    const getEducationalYearQuery = useQuery<GetEducationalYearWithSubjectsData, GetEducationalYearWithSubjectsVars>(GET_EDUCATIONAL_YEAR_WITH_SUBJECTS_QUERY,
        {variables: {id: id, subjectsPage: subjectsPage}},
    );

    const columns: ColumnsType<Subject> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
            render: (text, subject) => (
                <Space>
                    <div>{subject?.name}</div>
                    <div>
                        {subject.teacherId === currentUser?.id && <Tag color={'green'}>Мій</Tag>}
                        {subject.teachersHaveAccessCreatePosts?.some(t => t.id === currentUser?.id) && <Tag color={'cyan'}>Надано доступ</Tag>}
                    </div>
                </Space>
            ),
        },
        {
            title: 'Вчитель',
            dataIndex: 'teacher',
            key: 'teacher',
            render: (text, subject) => (
                <Link to={`../../teachers/${subject?.teacherId}`}>
                    {subject?.teacher?.lastName} {subject?.teacher?.firstName}
                </Link>
            ),
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, subject: Subject) => (
                // (currentUser?.id === subject.teacherId || currentUser?.role === Role.Administrator)
                //     ? <ButtonsVUR viewUrl={`../../subjects/${subject?.id}`} updateUrl={`../../subjects/update/${subject?.id}`}
                //                   onRemove={() => onRemove(subject?.id)}/>
                //     : <ButtonsVUR viewUrl={`../../subjects/${subject?.id}`}/>
                <ButtonsVUR viewUrl={`../../subjects/${subject?.id}`} updateUrl={`../../subjects/update/${subject?.id}`}/>
            ),
        },
    ];

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getEducationalYearQuery.loading)
        return <Loading/>;

    const educationalYear = getEducationalYearQuery.data?.getEducationalYear;
    return (
        <Space direction={'vertical'} size={20} style={{width: '100%'}}>
            <Title level={2}>Перегляд навчального року</Title>
            <Title level={3}>{educationalYear?.name}</Title>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Поточний:</td>
                    <td>
                        {educationalYear?.isCurrent
                            ? <Tag color="green">Так</Tag>
                            : <Tag color="red">Ні</Tag>
                        }
                    </td>
                </tr>
                <tr>
                    <td>Дата початку:</td>
                    <td>
                        <span>{educationalYear?.dateStart && stringToUkraineDate(educationalYear.dateStart)}</span>
                    </td>
                </tr>
                <tr>
                    <td>Дата кінця:</td>
                    <td>
                        <span>{educationalYear?.dateEnd && stringToUkraineDate(educationalYear.dateEnd)}</span>
                    </td>
                </tr>
                </tbody>
            </table>
            <Table
                title={() => <Title level={4}>Предмети</Title>}
                rowKey={'id'}
                loading={getEducationalYearQuery.loading}
                dataSource={getEducationalYearQuery.data?.getEducationalYear?.subjects.entities}
                columns={columns}
                pagination={{
                    total: getEducationalYearQuery.data?.getEducationalYear?.subjects.total,
                    onChange: setSubjectsPage,
                }}
            />
        </Space>
    );
};
