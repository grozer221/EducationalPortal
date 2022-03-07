import React, {useState} from 'react';
import {useQuery} from '@apollo/client';
import {Link, Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {GET_SUBJECT_WITH_POSTS_QUERY, GetSubjectWithPostsData, GetSubjectWithPostsVars} from '../../subjects.queries';
import {SubjectPostsIndex} from '../../../subjectPosts/components/SubjectPostsIndex/SubjectPostsIndex';
import {Space} from 'antd';
import Title from 'antd/es/typography/Title';
import '../../../../../../styles/table.css';

export const SubjectsView = () => {
    const params = useParams();
    const id = params.id as string;
    const [postsPage, setPostsPage] = useState(1);

    const getSubjectQuery = useQuery<GetSubjectWithPostsData, GetSubjectWithPostsVars>(GET_SUBJECT_WITH_POSTS_QUERY,
        {variables: {id: id, postsPage: postsPage}},
    );

    const refetchSubjectAsync = async () => {
        await getSubjectQuery.refetch({id: id, postsPage: postsPage});
    };

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getSubjectQuery.loading)
        return <Loading/>;

    const subject = getSubjectQuery.data?.getSubject;
    return (
        <Space direction={'vertical'} size={20} style={{width: '100%'}}>
            <Title level={2}>Переглад предмету</Title>
            <Title level={3}>{subject?.name}</Title>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Вчитель:</td>
                    <td>
                        <span>
                            <Link to={`../../teachers/${subject?.teacherId}`}>{subject?.teacher.firstName} {subject?.teacher.lastName}</Link>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>Навчальний рік:</td>
                    <td>
                        <span>
                            <Link to={`../../educational-years/${subject?.educationalYearId}`}>{subject?.educationalYear.name}</Link>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>Посилання:</td>
                    <td>
                        <span>{subject?.link}</span>
                    </td>
                </tr>
                </tbody>
            </table>
            {subject &&
            <SubjectPostsIndex
                subject={subject}
                refetchSubjectAsync={refetchSubjectAsync}
                postsPage={postsPage}
                setPostsPage={setPostsPage}
            />}
        </Space>
    );
};
