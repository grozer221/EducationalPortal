import React, {FC, useState} from 'react';
import {Card, message, Pagination, Row, Space} from 'antd';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {SubjectPostsCreate} from '../SubjectPostsCreate/SubjectPostsCreate';
import {Subject} from '../../../../../../graphQL/modules/subjects/subjects.types';
import {useMutation} from '@apollo/client';
import {
    REMOVE_SUBJECT_POST_MUTATION,
    RemoveSubjectPostData,
    RemoveSubjectPostVars
} from '../../../../../../graphQL/modules/subjectPosts/subjectPosts.mutations';
import {SubjectPostsUpdate} from '../SubjectPostsUpdate/SubjectPostsUpdate';
import {SubjectPost, SubjectPostType} from '../../../../../../graphQL/modules/subjectPosts/subjectPosts.types';
import {subjectPostTypeToTag} from '../../../../../../convertors/enumToTagConvertor';
import Title from 'antd/es/typography/Title';
import parse from 'html-react-parser';
import {stringToUkraineDatetime} from '../../../../../../convertors/stringToDatetimeConvertors';
import '../../../../../../styles/text.css';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {HomeOutlined} from "@ant-design/icons";
import {Homeworks} from "../../../homeworks/components/Homeworks/Homeworks";
import {Doughnut} from "react-chartjs-2";
import {Chart, registerables} from 'chart.js'
import s from './SubjectPostsIndex.module.css';

Chart.register(...registerables);

type Props = {
    subject: Subject,
    refetchSubjectAsync: () => void,
    postsPage: number,
    setPostsPage: (page: number) => void,
};

export const SubjectPostsIndex: FC<Props> = ({subject, refetchSubjectAsync, postsPage, setPostsPage}) => {
    const [isModalPostCreateVisible, setIsModalPostCreateVisible] = useState(false);
    const [isModalPostUpdateVisible, setIsModalPostUpdateVisible] = useState(false);
    const [inEditingPost, setInEditingPost] = useState<SubjectPost | null>(null);
    const [inViewHomeworksPost, setInViewHomeworksPost] = useState<SubjectPost | null>(null);

    const [removeSubjectPostMutation, removeSubjectPostMutationOptions] = useMutation<RemoveSubjectPostData, RemoveSubjectPostVars>(REMOVE_SUBJECT_POST_MUTATION);

    const onPostRemove = (postId: string) => {
        removeSubjectPostMutation({
            variables: {
                id: postId,
            },
        })
            .then(async (response) => {
                await refetchSubjectAsync();
            })
            .catch(e => {
                message.error(e.message);
            });
    };

    const onPostUpdate = (post: SubjectPost) => {
        setInEditingPost(post);
        setIsModalPostUpdateVisible(true);
    };

    return (
        <>
            <Space direction={'vertical'} style={{width: '100%'}} size={20}>
                {/*{(currentUser?.id === subject.teacherId || currentUser?.role === Role.Administrator) &&*/}
                {/*<span onClick={() => setIsModalPostCreateVisible(true)}>*/}
                {/*    <ButtonCreate>Створити пост</ButtonCreate>*/}
                {/*    </span>*/}
                {/*}*/}
                <span onClick={() => setIsModalPostCreateVisible(true)}>
                    <ButtonCreate>Створити пост</ButtonCreate>
                </span>
                {subject?.posts?.entities.map(post => (
                    <Card
                        key={post.id}
                        type={'inner'}
                        title={
                            <Space size={1}>
                                {subjectPostTypeToTag(post.type)}
                                <Title level={4}>{post.title}</Title>
                            </Space>
                        }
                        extra={
                            // (currentUser?.id === subject.teacherId || currentUser?.role === Role.Administrator) &&
                            // <ButtonsVUR
                            //     onUpdate={() => onPostUpdate(post)}
                            //     onRemove={() => onPostRemove(post.id)}
                            // />
                            <Space size={10}>
                                <HomeOutlined onClick={() => setInViewHomeworksPost(post)}/>
                                <ButtonsVUR
                                    onUpdate={() => onPostUpdate(post)}
                                    onRemove={() => onPostRemove(post.id)}
                                />
                            </Space>
                        }
                    >
                        <div>{parse(post.text)}</div>
                        {post.type === SubjectPostType.Homework &&
                            <Space size={1}>
                                <Doughnut
                                    data={{
                                        labels: post.statistics.map(s => s.key),
                                        datasets: [{
                                            data: post.statistics.map(s => s.value),
                                            backgroundColor: post.statistics.map(s => s.hashColor),
                                            hoverBackgroundColor: post.statistics.map(s => s.hashColor)
                                        }]
                                    }}
                                    options={{
                                        plugins: {
                                            legend: {
                                                display: true,
                                                position: 'right',
                                                labels: {
                                                    boxWidth: 10,
                                                    boxHeight: 10,
                                                },
                                                maxWidth: 100
                                            }
                                        },
                                    }} className={s.doughnut}/>
                            </Space>
                        }
                        <div className={'small'}>
                            <div>Створено: {stringToUkraineDatetime(post.createdAt)}, {post.teacher.lastName} {post.teacher.firstName}</div>
                            <div>Оновлено: {stringToUkraineDatetime(post.updatedAt)}</div>
                        </div>
                    </Card>
                ))}
                {subject?.posts?.total > 0 &&
                    <Row justify={'end'}>
                        <Pagination defaultCurrent={postsPage} onChange={setPostsPage} total={subject?.posts.total}/>
                    </Row>
                }
            </Space>
            <SubjectPostsCreate
                isModalPostCreateVisible={isModalPostCreateVisible}
                setIsModalPostCreateVisible={setIsModalPostCreateVisible}
                subjectId={subject.id}
                refetchSubjectAsync={refetchSubjectAsync}
            />
            {inEditingPost && <SubjectPostsUpdate
                isModalPostUpdateVisible={isModalPostUpdateVisible}
                setIsModalPostUpdateVisible={setIsModalPostUpdateVisible}
                refetchSubjectAsync={refetchSubjectAsync}
                inEditingPost={inEditingPost}
                setInEditingPost={setInEditingPost}
            />}
            {inViewHomeworksPost && <Homeworks
                homeworks={inViewHomeworksPost.homeworks}
                isModalHomeworksVisible={!!inViewHomeworksPost}
                setModalHomeworksInvisible={() => setInViewHomeworksPost(null)}/>}
        </>
    );
};